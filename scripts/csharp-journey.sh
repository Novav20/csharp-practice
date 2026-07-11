#!/usr/bin/env bash

set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
TARGET_FRAMEWORK="${TARGET_FRAMEWORK:-net10.0}"

print_header() {
  printf '\n%s\n' "========================================"
  printf '%s\n' "$1"
  printf '%s\n' "========================================"
}

list_week_dirs() {
  find "$ROOT_DIR" -maxdepth 1 -type d -name 'W[0-9][0-9]' | sort
}

latest_week_dir() {
  list_week_dirs | tail -n 1
}

next_week_dir() {
  local last_week week_number next_week
  last_week="$(latest_week_dir)"

  if [[ -z "$last_week" ]]; then
    printf '%s/W01\n' "$ROOT_DIR"
    return
  fi

  week_number="${last_week##*/W}"
  next_week="$((10#$week_number + 1))"
  printf '%s/W%02d\n' "$ROOT_DIR" "$next_week"
}

list_day_dirs() {
  local week_dir="$1"
  find "$week_dir" -mindepth 1 -maxdepth 1 -type d -name 'Day[0-9][0-9]' | sort
}

next_day_number() {
  local last_day day_number next_day
  last_day="$(find "$ROOT_DIR" -mindepth 2 -maxdepth 2 -type d -name 'Day[0-9][0-9]' | sort | tail -n 1)"

  if [[ -z "$last_day" ]]; then
    printf '1\n'
    return
  fi

  day_number="${last_day##*/Day}"
  next_day="$((10#$day_number + 1))"
  printf '%d\n' "$next_day"
}

next_day_dir() {
  local week_dir="$1" day_number
  day_number="$(next_day_number)"
  printf '%s/Day%02d\n' "$week_dir" "$day_number"
}

create_project() {
  local week_choice week_dir project_dir project_name

  print_header "Crear nuevo proyecto"
  printf '1) Crear en la ultima semana\n'
  printf '2) Crear una nueva semana\n'
  read -rp 'Elige una opcion [1-2]: ' week_choice

  case "$week_choice" in
    1)
      week_dir="$(latest_week_dir)"
      if [[ -z "$week_dir" ]]; then
        week_dir="$(next_week_dir)"
      fi
      ;;
    2)
      week_dir="$(next_week_dir)"
      ;;
    *)
      printf 'Opcion invalida.\n' >&2
      return 1
      ;;
  esac

  mkdir -p "$week_dir"
  project_dir="$(next_day_dir "$week_dir")"
  project_name="$(basename "$project_dir")"

  if [[ -e "$project_dir" ]]; then
    printf 'El proyecto ya existe: %s\n' "$project_dir" >&2
    return 1
  fi

  printf '\nCreando %s dentro de %s...\n' "$project_name" "$(basename "$week_dir")"
  dotnet new console \
    --framework "$TARGET_FRAMEWORK" \
    --name "$project_name" \
    --output "$project_dir" \
    --force

  printf '\nProyecto creado en %s\n' "$project_dir"
}

run_project() {
  local projects=() project_dirs=() project_dir project_csproj index choice

  while IFS= read -r project_csproj; do
    [[ -n "$project_csproj" ]] || continue
    project_dir="$(dirname "$project_csproj")"
    project_dirs+=("$project_dir")
    projects+=("${project_dir#"$ROOT_DIR"/}")
  done < <(find "$ROOT_DIR" -path '*/Day*/Day*.csproj' -type f | sort)

  if [[ ${#projects[@]} -eq 0 ]]; then
    printf 'No se encontraron proyectos de consola.\n' >&2
    return 1
  fi

  print_header "Ejecutar proyecto existente"
  local i=1
  for project_dir in "${projects[@]}"; do
    printf '%d) %s\n' "$i" "$project_dir"
    ((i++))
  done

  read -rp 'Elige un proyecto: ' choice
  if ! [[ "$choice" =~ ^[0-9]+$ ]] || (( choice < 1 || choice > ${#project_dirs[@]} )); then
    printf 'Seleccion invalida.\n' >&2
    return 1
  fi

  project_dir="${project_dirs[$((choice - 1))]}"
  printf '\nEjecutando %s...\n\n' "${project_dir#"$ROOT_DIR"/}"
  dotnet run --project "$project_dir"
}

while true; do
  print_header "CLI de C#"
  printf '1) Crear nuevo proyecto de consola\n'
  printf '2) Ejecutar proyecto existente\n'
  printf '3) Salir\n'
  read -rp 'Elige una opcion [1-3]: ' main_choice

  case "$main_choice" in
    1)
      create_project
      ;;
    2)
      run_project
      ;;
    3)
      exit 0
      ;;
    *)
      printf 'Opcion invalida.\n' >&2
      ;;
  esac

  printf '\n'
done